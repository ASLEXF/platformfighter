public interface IWeapon
{
    void StartOneStrike();

    void EndOneStrike();
}

public interface IItem
{

}

public interface IPlayer
{
    public PlayerAttacked GetPlayerAttacked();
}