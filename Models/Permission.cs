using System;

namespace UserService.Models {
    public class Permission {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
    }
}