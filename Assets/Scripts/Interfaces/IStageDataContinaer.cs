using Stages;
using Stages.Map;

namespace Interfaces
{
    public interface IStageDataContainer: IDataContainer<StageDatabaseSO>
    {
        public StageDatabaseSO notes { get; set; }
    }
}