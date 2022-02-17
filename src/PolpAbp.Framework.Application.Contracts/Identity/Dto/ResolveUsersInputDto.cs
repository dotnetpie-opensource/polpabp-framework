using System;
using System.ComponentModel.DataAnnotations;
using Polpware.ComponentModel.DataAnnotations.Validators;

namespace PolpAbp.Framework.Identity.Dto
{
    public class ResolveUsersInputDto
    {
        [Required]
        [HasSomeMember(1)]
        public Guid[] Ids { get; set; }
    }
}
