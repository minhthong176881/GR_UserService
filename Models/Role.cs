using System;
using System.Collections.Generic;

namespace UserService.Models {
    public class Role {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName {get; set;}
        public string Description { get; set; }
        // public List<int> GrantedPermissions { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}