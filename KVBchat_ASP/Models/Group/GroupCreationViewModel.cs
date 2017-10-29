using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KVBchat_ASP.Models.Group
{
    public class GroupCreationViewModel
    {
        public int CreatorId { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> SelectedMembers { get; set; }

        public IEnumerable<SelectListItem> Members { get; set; }
    }
}