﻿using System;
using System.Linq;
using Timespinner.GameAbstractions;
using Timespinner.GameAbstractions.Saving;
using Timespinner.GameStateManagement.ScreenManager;
using TsRandomizer.Extensions;
using TsRandomizer.IntermediateObjects;
using TsRandomizer.Randomisation;
using TsRandomizer.Screens.Menu;
using TsRandomizer.Screens.SeedSelection;

using System.Collections;


namespace TsRandomizer.Screens.SeedSettings
{
	[TimeSpinnerType("Timespinner.GameStateManagement.Screens.PauseMenu.JournalMenuScreen")]
	// ReSharper disable once UnusedMember.Global
	class SeedSettingsScreen : Screen
	{
		static readonly Type MenuEntryType =
			TimeSpinnerType.Get("Timespinner.GameStateManagement.MenuEntry");
		static readonly Type MenuEntryCollectionType =
			TimeSpinnerType.Get("Timespinner.GameStateManagement.MenuEntryCollection");
		static readonly Type JournalMenuType =
			TimeSpinnerType.Get("Timespinner.GameStateManagement.Screens.PauseMenu.JournalMenuScreen");

		readonly SeedSelectionMenuScreen seedSelectionScreen;

		bool IsUsedAsSeedSettingsMenu => seedSelectionScreen != null;

		public override void Initialize(ItemLocationMap itemLocationMap, GCM gameContentManager)
		{
			if (!IsUsedAsSeedSettingsMenu)
				return;

			Dynamic._menuTitle = "Seed Settings";

			var menuEntryList = new object[0].ToList(MenuEntryType);
			var menuCollectionList = new object[0].ToList(MenuEntryCollectionType);

			/*
			var menus = ((IList)((object)Dynamic._primaryMenuCollection).AsDynamic()._entries)
				.Cast<object>()
				.ToList(MenuEntryType);
			var placeHolderMenu = menus[4];

			placeHolderMenu.AsDynamic().Text = "Stats";
			menuEntryList.Add(placeHolderMenu);
			placeHolderMenu.AsDynamic().Text = "Enemies";
			menuEntryList.Add(placeHolderMenu);
			placeHolderMenu.AsDynamic().Text = "Shops";
			menuEntryList.Add(placeHolderMenu);
			placeHolderMenu.AsDynamic().Text = "Sprites";
			menuEntryList.Add(placeHolderMenu);
			placeHolderMenu.AsDynamic().Text = "Other";
			menuEntryList.Add(placeHolderMenu);*/

			var scalingMenu = MenuEntry.Create("Stats", () => { });
			scalingMenu.AsDynamic().Description = "Settings related to stat scaling.";
			scalingMenu.AsDynamic().IsCenterAligned = false;

			var enemyMenu = MenuEntry.Create("Enemies", () => { });
			enemyMenu.Description = "Settings related to enemy placement and drops.";
			enemyMenu.IsCenterAligned = false;

			var shopMenu = MenuEntry.Create("Shops", () => { });
			shopMenu.Description = "Settings related to shop inventory.";
			shopMenu.IsCenterAligned = false;

			var spriteMenu = MenuEntry.Create("Sprites", () => { }, false);
			spriteMenu.Description = "Settings related to sprite replacement.";
			spriteMenu.IsCenterAligned = false;

			var otherMenu = MenuEntry.Create("Other", () => { });
			otherMenu.Description = "Various other settings.";
			otherMenu.IsCenterAligned = false;

			menuEntryList.Add(scalingMenu.AsTimeSpinnerMenuEntry());
			menuEntryList.Add(enemyMenu.AsTimeSpinnerMenuEntry());
			menuEntryList.Add(shopMenu.AsTimeSpinnerMenuEntry());
			menuEntryList.Add(spriteMenu.AsTimeSpinnerMenuEntry());
			menuEntryList.Add(otherMenu.AsTimeSpinnerMenuEntry());

			((object)Dynamic._primaryMenuCollection).AsDynamic()._entries = menuEntryList;

			var collections = ((IList)Dynamic._subMenuCollections)
				.Cast<object>()
				.ToList(MenuEntryCollectionType);
			var bestiary = collections[4];

			menuCollectionList.Add(bestiary);
			menuCollectionList.Add(bestiary);
			menuCollectionList.Add(bestiary);
			menuCollectionList.Add(bestiary);
			menuCollectionList.Add(bestiary);
			Dynamic._subMenuCollections = menuCollectionList;
		}

		public SeedSettingsScreen(ScreenManager screenManager, GameScreen passwordMenuScreen) : base(screenManager, passwordMenuScreen)
		{
			seedSelectionScreen = screenManager.FirstOrDefault<SeedSelectionMenuScreen>();
		}

		public static GameScreen Create(ScreenManager screenManager, SeedOptionsCollection options)
		{
			void Noop() { }
			return (GameScreen)Activator.CreateInstance(JournalMenuType, GameSave.DemoSave, screenManager.Dynamic.GCM, (Action)Noop);
		}
	}
}
