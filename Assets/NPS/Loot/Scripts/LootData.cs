using System;

namespace NPS.Loot
{
    [System.Serializable]
    public class LootData
    {
        public LootType Type;
        public ILootData Data;

        public LootData()
        {

        }

        public LootData(string content)
        {
            string[] str = content.Split(';');
            Enum.TryParse(str[0], out LootType lootType);
            Type = lootType;

            switch (lootType)
            {
                case LootType.Currency:
                    Data = new CurrencyData(content);
                    break;
            }
        }
        public LootData Clone()
        {
            LootData clone = new LootData();
            clone.Type = Type;
            clone.Data = Data.Clone();
            return clone;
        }

        public bool Same(LootData data)
        {
            return Type == data.Type && data.Data.Same(Data);
        }
    }
}