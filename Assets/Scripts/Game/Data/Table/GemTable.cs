using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemTable : IDataTable
{
    public List<GemEntity> List = new List<GemEntity>();

    public void GetDatabase()
    {
        List.Clear();
        DB_Gem.ForEachEntity(entity => {
            if (entity != null)
            {
                var data = new GemEntity(entity);
                List.Add(data);
            }
        });
    }
}
