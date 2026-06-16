using UnityEngine;

namespace GameSystemsScripts.EconomyScripts
{
    public class PlayerCurrencyMechanic
    {
        public PlayerCurrencyComponent Component;

        public PlayerCurrencyMechanic(PlayerCurrencyComponent component)
        {
            Component = component;
        }

        public void Load()
        {
            Component.Balance = PlayerPrefs.GetInt(PlayerCurrencyComponent.PlayerPrefsCurrencyKey, 0);
        }

        public int GetBalance()
        {
            return Component.Balance;
        }

        public void AddCurrency(int value)
        {
            if (value <= 0)
            {
                return;
            }

            Component.Balance += value;
            Save();
        }

        public bool TrySpend(int value)
        {
            if (value <= 0)
            {
                return true;
            }

            if (Component.Balance < value)
            {
                return false;
            }

            Component.Balance -= value;
            Save();
            return true;
        }

        public void Save()
        {
            PlayerPrefs.SetInt(PlayerCurrencyComponent.PlayerPrefsCurrencyKey, Component.Balance);
        }
    }
}
