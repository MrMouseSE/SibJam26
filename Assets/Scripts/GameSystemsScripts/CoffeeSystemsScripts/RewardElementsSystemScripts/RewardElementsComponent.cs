using LevelAchievementsScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.RewardElementsSystemScripts
{
    public class RewardElementsComponent
    {
        public DaysAchievementsDescription DayAchievementsDescription;
        public RewardShopDescription RewardShopDescription;
        public RewardShopContainer RewardShopContainer;

        public bool IsRewardStateInitialized;
        public bool IsContinuePressed;
        public bool IsBoosterPurchasedThisState;
        public bool IsIngredientPurchasedThisState;
        public bool IsSlotPurchasedThisState;
        public bool IsBoosterBuyRequested;
        public bool IsIngredientBuyRequested;
        public bool IsSlotBuyRequested;

        public RewardElementsComponent(DaysAchievementsDescription dayAchievementsDescription, RewardShopDescription rewardShopDescription)
        {
            DayAchievementsDescription = dayAchievementsDescription;
            RewardShopDescription = rewardShopDescription;
        }
    }
}
