public class Constant
{
    public const float GRID_SIZE = 1.0f;
    public const string PLAYER_TAG = "Player";
    public const string REVERSE_PLAYER_TAG = "ReversePlayer";
    public const string CHECK_POINT_TAG = "CheckPoint";


    public const int OBSTACLE_LAYER = 3;
    public const int LAND_LAYER = 6;
    public const int FLOWER_LAYER = 7;
    public const int DEFAULT_LAYER = 0;

    public const int OBSTACLE_LAYER_MASK = 1 << OBSTACLE_LAYER;
    public const int LAND_LAYER_MASK = 1 << LAND_LAYER;
    public const int FLOWER_LAYER_MASK = 1 << FLOWER_LAYER;

}
public enum SceneType
{
    START_SCENE = 0,
    HOME_SCENE = 1,
    GAME_PLAY_SCENE = 2
}