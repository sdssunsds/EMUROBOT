using EMU.Interface;

namespace Project
{
    public interface IAlgorithmInterface : IProject
    {
        string RedisThreadName { get; set; }
        string inParName1 { get; set; }
        string inParName2 { get; set; }
        string inParName3 { get; set; }
        string inParObject { get; set; }
        string outParName { get; set; }
        string JsonErrorChange(string json);
        void ResultBack(string id, string json);
        void RunInterface(string url, string inputName, string outputName, int sleep, string pwd);
    }
}
