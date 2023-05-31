using Heatmap.Scripts.Recorder;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class PlayerHeatmapRecorder
{
    private IRecorder recorder;
    private readonly Transform _player;
    private int _levelIndex;
    private readonly PlayerHeatmapRecordeData _playerHeatmapRecordeData;

    public PlayerHeatmapRecorder(int levelIndex, PlayerHeatmapRecordeData playerHeatmapRecordeData)
    {
        _levelIndex = levelIndex;
        _playerHeatmapRecordeData = playerHeatmapRecordeData;
        _player = Object.FindObjectOfType<PlayerMovement>().FeetCollider.transform;

        var path =
            $"{SceneManager.GetActiveScene().name}/{playerHeatmapRecordeData.PlayerName}_{playerHeatmapRecordeData.GetRecordeNumber(_levelIndex)}.json";
        var localPath = $"Assets/TestHeatmap/{path}";
        recorder = RecorderFactory.Instance.GetFirebaseRecorder(
            new RecordeSettingContainer("playerPosition", 0.02f, GetPlayerPos), localPath,
            path);
    }

    private Vector3 GetPlayerPos() => _player.position;

    public void Start()
    {
        recorder.Play();
    }

    public void Finish()
    {
        recorder.Complete();
        _playerHeatmapRecordeData.AddRecordeNumber(_levelIndex);
    }
}