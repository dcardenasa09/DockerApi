using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Lib.Models;

public abstract class AuditableEntity {
    [Column("created_at")]
    public DateTime Created { get; set; }

    [Column("create_offset")]
    public int? CreateOffset { get; set; }

    [Column("created_by")]
    public int? CreatedBy { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("update_offset")]
    public int? UpdateOffset { get; set; }

    [Column("updated_by")]
    public int? UpdatedBy { get; set; }
}