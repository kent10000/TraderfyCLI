﻿namespace Traderfy.TarkovSharp;

public enum LanguageCode {
    cz,
    de,
    en,
    es,
    fr,
    hu,
    ru,
    tr,
    zh
}

public enum ItemCategoryName {
    Ammo,
    AmmoContainer,
    ArmBand,
    Armor,
    ArmoredEquipment,
    AssaultCarbine,
    AssaultRifle,
    AssaultScope,
    AuxiliaryMod,
    Backpack,
    Barrel,
    BarterItem,
    Battery,
    Bipod,
    BuildingMaterial,
    ChargingHandle,
    ChestRig,
    CombMuzzleDevice,
    CombTactDevice,
    CommonContainer,
    CompactReflexSight,
    Compass,
    CylinderMagazine,
    Drink,
    Drug,
    Electronics,
    Equipment,
    EssentialMod,
    FaceCover,
    Flashhider,
    Flashlight,
    Food,
    FoodAndDrink,
    Foregrip,
    Fuel,
    FunctionalMod,
    GasBlock,
    GearMod,
    GrenadeLauncher,
    Handguard,
    Handgun,
    Headphones,
    Headwear,
    HouseholdGoods,
    Info,
    Ironsight,
    Jewelry,
    Key,
    KeyMechanical,
    Keycard,
    Knife,
    LockingContainer,
    Lubricant,
    Machinegun,
    Magazine,
    Map,
    MarksmanRifle,
    MedicalItem,
    MedicalSupplies,
    Medikit,
    Meds,
    Money,
    Mount,
    MuzzleDevice,
    NightVision,
    Other,
    PistolGrip,
    PortContainer,
    PortableRangeFinder,
    Receiver,
    ReflexSight,
    RepairKits,
    Revolver,
    SMG,
    Scope,
    Shotgun,
    Sights,
    Silencer,
    SniperRifle,
    SpecialItem,
    SpecialScope,
    SpringDrivenCylinder,
    Stimulant,
    Stock,
    ThermalVision,
    ThrowableWeapon,
    Tool,
    VisObservDevice,
    Weapon,
    WeaponMod
}

public enum ItemType {
    ammo,
    ammoBox,
    any,
    armor,
    backpack,
    barter,
    container,
    glasses,
    grenade,
    gun,
    headphones,
    helmet,
    injectors,
    keys,
    markedOnly,
    meds,
    mods,
    noFlea,
    pistolGrip,
    preset,
    provisions,
    rig,
    suppressor,
    wearable
}

public enum TraderName {
    prapor, 
    therapist,
    fence,
    skier,
    peacekeeper,
    mechanic,
    ragman,
    jaeger
}

public enum ItemSourceName {
    prapor, 
    therapist,
    fence,
    skier,
    peacekeeper,
    mechanic,
    ragman,
    jaeger,
    fleaMarket
}

public enum RequirementType {
    playerLevel,
    loyaltyLevel,
    questCompleted,
    stationLevel
}

public enum StatusCode {
    OK,
    Updating,
    Unstable,
    Down
}