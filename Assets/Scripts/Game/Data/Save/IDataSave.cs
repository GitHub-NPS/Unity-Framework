public interface IDataSave
{
    string Key { get; }
    void Fix();
    void Save();
}
