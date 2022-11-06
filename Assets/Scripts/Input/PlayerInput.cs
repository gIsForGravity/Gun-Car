using Fusion;

public struct PlayerInput : INetworkInput {
    public NetworkBool Forward;
    public NetworkBool Backward;
    public NetworkBool Left;
    public NetworkBool Right;

    public NetworkBool Fire;
    public NetworkBool ADS;
    public float xChange;
    public float yChange;
}