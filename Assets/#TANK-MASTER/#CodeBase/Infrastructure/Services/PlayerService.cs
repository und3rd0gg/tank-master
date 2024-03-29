using System;
using TankMaster.Gameplay.Actors.MainPlayer;

namespace TankMaster.Infrastructure.Services
{
  public interface IPlayerService
  {
    Player GetPlayer();
    void SetPlayer(Player player);
  }

  public class PlayerService : IPlayerService
  {
    private Player _player;
    
    public Player GetPlayer() {
      return _player;
    }
    
    public void SetPlayer(Player player) {
      if (_player != null) {
        throw new Exception("Player already set");
      }
      
      _player = player;
    }
  }
}