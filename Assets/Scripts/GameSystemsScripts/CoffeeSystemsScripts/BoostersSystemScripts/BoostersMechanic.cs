using System.Collections.Generic;
using System.Linq;
using GameObjectFactories;
using GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.FillRecipeScripts;
using ScenesOperatingScripts;
using UnityEngine;

namespace GameSystemsScripts.CoffeeSystemsScripts.BoostersSystemScripts
{
    public class BoostersMechanic : IGameMechanic
    {
        private const string BoosterPurchasedKeyPrefix = "PurchasedBooster_";
        public BoostersComponent Component;

        public BoostersMechanic(BoostersComponent component)
        {
            Component = component;
        }

        public void InitializeBoosters()
        {
            if (Component.BoostersDescription == null || Component.BoostersDescription.Boosters == null)
            {
                Component.AllBoosters = new List<BoosterItem>();
                Component.ActiveBoosters = new List<BoosterItem>();
                return;
            }

            Component.AllBoosters = Component.BoostersDescription.Boosters
                .Select(BoosterItemFactory.CreateFromDescription)
                .ToList();

            foreach (var booster in Component.AllBoosters)
            {
                if (IsBoosterPurchased(booster.BoosterName))
                {
                    booster.IsEnabled = true;
                }
            }

            Component.ActiveBoosters = Component.AllBoosters
                .Where(booster => booster.IsEnabled)
                .ToList();
        }

        public bool IsBoosterPurchased(string boosterName)
        {
            if (string.IsNullOrEmpty(boosterName))
            {
                return false;
            }

            return PlayerPrefs.GetInt(GetBoosterPurchasedKey(boosterName), 0) == 1;
        }

        public bool TryUnlockBoosterPermanent(string boosterName)
        {
            if (string.IsNullOrEmpty(boosterName))
            {
                return false;
            }

            if (IsBoosterPurchased(boosterName))
            {
                return false;
            }

            var booster = Component.AllBoosters.FirstOrDefault(x => x.BoosterName == boosterName);
            if (booster == null)
            {
                return false;
            }

            PlayerPrefs.SetInt(GetBoosterPurchasedKey(boosterName), 1);
            return AddBooster(booster);
        }

        public bool AddBooster(BoosterItem booster)
        {
            if (Component.ActiveBoosters.Contains(booster))
            {
                return false;
            }

            booster.IsEnabled = true;
            Component.ActiveBoosters.Add(booster);
            return true;
        }

        public bool RemoveBooster(BoosterItem booster)
        {
            if (booster == null)
            {
                return false;
            }

            if (!Component.ActiveBoosters.Remove(booster))
            {
                return false;
            }

            booster.IsEnabled = false;
            return true;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            var fillRecipeSystem = (FillRecipeSystem)gameSystemsHandler.GetGameSystem(typeof(FillRecipeSystem));
            float multiplier = 1f;

            foreach (var booster in Component.ActiveBoosters)
            {
                multiplier *= booster.GetAppliedMultiplier(fillRecipeSystem.Component.RecipeContainers);
            }

            Component.CurrentMultiplier = multiplier;
        }

        public void DisposeMechanic()
        {
        }

        private static string GetBoosterPurchasedKey(string boosterName)
        {
            return BoosterPurchasedKeyPrefix + boosterName;
        }
    }
}
