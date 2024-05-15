﻿using GenericModConfigMenu;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;

namespace SpecialPowerUtilities.Config;

public sealed class ModConfig
{
    public bool EnableCategories { get; set; } = true;
    
    public bool ParseModNames { get; set; } = true;
    
    public bool EnableMiscCategory { get; set; } = true;
    
    public bool UseVanillaMenu { get; set; } = false;
    
    public bool StripelessBooks { get; set; } = true;

    public ModConfig()
    {
        Init();
    }

    private void Init()
    {
        this.EnableCategories = true;
        this.ParseModNames = true;
        this.EnableMiscCategory = true;
        this.UseVanillaMenu = false;
        this.StripelessBooks = true;
    }

    public void SetupConfig(IGenericModConfigMenuApi configMenu, IManifest ModManifest, IModHelper Helper)
    {
        configMenu.Register(
            mod: ModManifest,
            reset: Init,
            save: () => Helper.WriteConfig(this)
        );
        
        configMenu.AddBoolOption(
            mod: ModManifest,
            name: i18n.Config_EnableCategories_Name,
            tooltip: i18n.Config_EnableCategories_Description,
            getValue: () => this.EnableCategories,
            setValue: value => this.EnableCategories = value
        );
        
        configMenu.AddBoolOption(
            mod: ModManifest,
            name: i18n.Config_ParseModNames_Name,
            tooltip: i18n.Config_ParseModNames_Description,
            getValue: () => this.ParseModNames,
            setValue: value => this.ParseModNames = value
        );
        
        configMenu.AddBoolOption(
            mod: ModManifest,
            name: i18n.Config_EnableMiscCategory_Name,
            tooltip: i18n.Config_EnableMiscCategory_Description,
            getValue: () => this.EnableMiscCategory,
            setValue: value => this.EnableMiscCategory = value
        );
        
        configMenu.AddBoolOption(
            mod: ModManifest,
            name: i18n.Config_StripelessBooks_Name,
            tooltip: i18n.Config_StripelessBooks_Description,
            getValue: () => this.StripelessBooks,
            setValue: (value) =>
            {
                this.StripelessBooks = value;
                Helper.GameContent.InvalidateCache(asset => asset.DataType == typeof(Texture2D) && asset.Name.IsEquivalentTo("LooseSprites/Book_Animation")); 
            });
        
        configMenu.AddBoolOption(
            mod: ModManifest,
            name: i18n.Config_UseVanillaMenu_Name,
            tooltip: i18n.Config_UseVanillaMenu_Description,
            getValue: () => this.UseVanillaMenu,
            setValue: value => this.UseVanillaMenu = value
        );
    }
}