namespace DxBlazorApplication1.Data {
    public class User {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public IEnumerable<string> Privileges { get; set; } = new List<string>();
    }
}
