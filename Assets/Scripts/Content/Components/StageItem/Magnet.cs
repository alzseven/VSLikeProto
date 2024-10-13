namespace Content.Components.StageItem
{
    public class Magnet : StageItemComponent
    {
        protected override void Use()
        {
            foreach (var exp in ExpPickup.EnabledExpPickups)
            {
                exp.isMovingToPlayer = true;
            }
        }
    }
}