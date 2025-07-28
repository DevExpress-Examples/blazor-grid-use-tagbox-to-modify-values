namespace DxBlazorApplication1.Data {
    public class DataService {
        public static List<User> GetUsers() {
            string[] sampleNames = {
                "Alice Johnson", "Bob Smith", "Charlie Lee", "Dana White", "Ethan Brown",
                "Fiona Scott", "George Miller", "Hannah Davis", "Ian Thompson", "Julia Clark",
                "Kevin Wilson", "Laura Adams", "Mike Green", "Nina Baker", "Owen Hall",
                "Paula Turner", "Quinn Morgan", "Rachel Evans", "Steve Walker", "Tina Young"
            };
            string[] positions = {
                "System Administrator", "Content Editor", "Data Analyst", "Support Engineer",
                "HR Manager", "QA Specialist", "IT Manager", "Operations Lead", "DevOps Engineer", "UI Designer"
            };
            var privilegies = new[] {
                new[] { "Manage Users", "Audit Logs" },
                new[] { "Edit Content", "Access Reports" },
                new[] { "Approve Requests" }
            };

            return Enumerable.Range(0, 19).Select(i => new User {
                Id = i,
                FullName = sampleNames[i],
                Position = positions[i % positions.Length],
                Privileges = new List<string>(privilegies[i % 3])
            }).ToList();
        }
    }
}
