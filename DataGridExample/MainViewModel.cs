using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridExample {
    internal class MainViewModel {
        public class User(string account, string username, string password, bool isActivated = false) {
            public string Account { get; set; } = account;
            public string Username { get; set; } = username;
            public string Password { get; set; } = password;
            public bool IsActivated { get; set; } = isActivated;
        }

        internal ObservableCollection<User> Users { get; set; } = [
            new User("001", "Tom", "t_o_m", true),
            new User("002", "Jerry", "j"),
        ];

        internal void AddOne() {
            var random = new Random();
            Users.Add(new(random.Next().ToString(), random.Next().ToString(), random.Next().ToString()));
        }

        internal void RemoveOne() {
            if (Users.Count == 0) return;
            Users.RemoveAt(Users.Count - 1);
        }
    }
}
