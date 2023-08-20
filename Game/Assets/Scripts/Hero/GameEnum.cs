

public enum SkillType
{
    Shield, // Khiên + x%
    ReduceManaSkill, // Mana skill - x
    Healing, // Mỗi giây hồi x% máu
    AtkResistance, // Sát thương vật lý nhận phải - x%
    Riposte, // Phản công x% sát thương đã nhận,
    SpResistance, // Sát thương phép nhận phải - x%

}

public enum AttributeType
{
    HP, // Máu
    ATK, // Damage vật lý
    ATKSP, // Tốc độ đánh trên giây
    SP, // Damage phép thuật
    MP, // Mana point khởi tạo
    MaxMP, // Max mana point
    MSP, // Tốc độ di chuyển
    RANGE // Tầm đánh (ô)
}

public enum HeroClass
{
    Range,
    Fighter,
    Tank,
    Assassin,
    Support
}

public enum StarType
{
    Copper,
    Silver,
    Gold,
    Diamond
}

public enum ResourceType
{
    Gold,
    Gem
}
