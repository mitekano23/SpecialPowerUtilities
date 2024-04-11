﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using HarmonyLib;
using SpecialPowerUtilities.Helpers;
using SpecialPowerUtilities.Menus;
using SpecialPowerUtilities.Models;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.GameData.Powers;
using StardewValley.Menus;

namespace SpecialPowerUtilities
{
    public class ModEntry : Mod
    {
        internal static IModHelper ModHelper { get; set; } = null!;
        internal static IMonitor ModMonitor { get; set; } = null!;
        internal static Harmony harmony { get; set; } = null!;
        
        internal static List<KeyValuePair<string, SPUData>> PowerExtensions = new();
        
        public override void Entry(IModHelper helper)
        {
            ModHelper = helper;
            ModMonitor = Monitor;
            harmony = new Harmony(ModManifest.UniqueID);
            harmony.PatchAll();
            Helper.Events.Display.MenuChanged += OnMenuChange;
        }
        
        private void OnMenuChange(object sender, MenuChangedEventArgs e)
        {
            if (e.NewMenu is not GameMenu menu)
                return;
            // log the type of menu it is
            Loggers.Log($"Menu: {e.NewMenu.GetType().Name}");
            int powersTabIndex = 0;
            for (int i = 0; i < menu.pages.Count; i++)
            {
                if (menu.pages[i] is not PowersTab powersTab)
                    continue;
                powersTabIndex = i;
                break;
            }
            Loggers.Log($"Found PowersTab at index {powersTabIndex}");
            IClickableMenu oldTab = menu.pages[powersTabIndex];
            try
            {
                menu.pages[powersTabIndex] = new SPUTab(oldTab.xPositionOnScreen, oldTab.yPositionOnScreen, oldTab.width, oldTab.height);
            }
            catch (Exception ex)
            {
                Loggers.Log($"Failed to replace PowersTab: {ex.Message}");
            }
        }
    }
}