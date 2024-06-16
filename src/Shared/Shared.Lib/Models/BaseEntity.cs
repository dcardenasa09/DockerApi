using Shared.Lib.Interfaces;

namespace Shared.Lib.Models;

public abstract class BaseEntity : IDTO {
    public int Id { get; set; }
}