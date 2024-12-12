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

public interface IPlayerInput
{
    public PlayerL GetPlayerL();
    public PlayerR GetPlayerR();
}