using System.Collections.Generic;

public static class AIConfiguration {
    public static HashSet<AIState> trashSpawnStateSet = new HashSet<AIState>() {
        AIState.MovingToPoint,
        AIState.WalkingAway,
        AIState.Sitting,
        AIState.StandingUp
    };
}