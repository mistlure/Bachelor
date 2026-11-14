namespace Bachelor.Components
{
    /// <summary>
    /// Short-lived request to perform an action on a target entity.
    /// Created by input systems, consumed and destroyed by ActionExecutionSystem.
    /// </summary>
    public struct ActionRequestComponent : IComponent
    {
        public string ActionId;
        public int TargetEntityId;
    }
}
