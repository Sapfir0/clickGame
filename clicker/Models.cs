
using System;
using SQLite;

namespace clicker {
    class ExitState {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime ExitDateTime { get; set; }
    }
}