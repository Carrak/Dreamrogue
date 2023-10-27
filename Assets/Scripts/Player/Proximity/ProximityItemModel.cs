public class ProximityItemModel
{
    public PickableWeapon HighlightedPickableWeapon { get; private set; }

    public void ChangeWeapon(PickableWeapon weapon)
    {
        HighlightedPickableWeapon = weapon;
    }
}