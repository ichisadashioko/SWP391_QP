using QuizPractice.Domain.Enums;

namespace QuizPractice.Domain.Entities;

/// <summary>A value of one of the 8 fixed setting types: roles, menus, categories, levels... (BR-16).</summary>
public class Setting
{
    public int Id { get; set; }
    public SettingType Type { get; set; }
    public string Value { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public string? Description { get; set; }
    public ActiveStatus Status { get; set; } = ActiveStatus.Active;
}
