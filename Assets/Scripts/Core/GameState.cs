using UnityEngine;
public enum GameState { Boot, Menu, Running, Paused, GameOver }
public sealed class GameStateController : MonoBehaviour { public GameState Current { get; private set; } = GameState.Boot; public void Set(GameState state)=>Current=state; }