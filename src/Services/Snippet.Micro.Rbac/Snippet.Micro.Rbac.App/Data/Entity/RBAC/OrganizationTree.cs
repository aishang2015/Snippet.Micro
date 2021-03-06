//------------------------------------------------------------------------------
// 生成时间 2021-09-01 10:11:39
//------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Snippet.Micro.Rbac.App.Data.Entity.RBAC
{
    [Comment("组织树")]
    [Table("t_rbac_organization_tree")]
    public class OrganizationTree
    {
        [Comment("主键")]
        [Column("id")]
        public int Id { get; set; }

        [Comment("祖先")]
        [Column("ancestor")]
        public int Ancestor { get; set; }

        [Comment("后代")]
        [Column("descendant")]
        public int Descendant { get; set; }

        [Comment("深度")]
        [Column("length")]
        public int Length { get; set; }
    }
}