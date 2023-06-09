using System.Collections.Generic;

public static class AIConfiguration {
    public static HashSet<AIState> coroutineSpawnState = new HashSet<AIState>() {
        AIState.MovingToPoint,
        AIState.WalkingAway,
    };

    public static HashSet<AIState> instantSpawnStateSet = new HashSet<AIState>() {
        AIState.Sitting,
    };
}