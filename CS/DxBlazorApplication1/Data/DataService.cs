using static DxBlazorApplication1.Pages.Index;

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

            return Enumerable.Range(1, 20).Select(i => new User {
                Id = i,
                FullName = sampleNames[i - 1],
                Position = positions[i % positions.Length],
                Privileges = i % 3 == 0
                    ? new List<string> { "Manage Users", "Audit Logs" }
                    : i % 3 == 1
                        ? new List<string> { "Edit Content", "Access Reports" }
                        : new List<string> { "Approve Requests" }
            }).ToList();
        }
    }
}
