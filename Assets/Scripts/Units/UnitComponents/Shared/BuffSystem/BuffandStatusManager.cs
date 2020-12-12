using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Units.UnitComponents.Shared
{
    public class BuffandStatusManager : MonoBehaviour
    {
        private List<Buff> buffs;
        public List<Buff> Buffs { get;}

        private void Awake()
        {
            buffs = new List<Buff>();
        }

        // helper function to find a buff index
        public int GetBuffIndexByName(string buffName)
        {
            // (avoid FindIndex to minimize allocations)
            for (int i = 0; i < buffs.Count; ++i)
                if (buffs[i].name == buffName)
                    return i;
            return -1;
        }
        // helper function to add or refresh a buff
        public void AddOrRefreshBuff(Buff buff)
        {
            // reset if already in buffs list, otherwise add
            int index = GetBuffIndexByName(buff.name);
            if (index != -1) buffs[index] = buff;
            else buffs.Add(buff);
        }

        // helper function to remove all buffs that ended
        void CleanupBuffs()
        {
            for (int i = 0; i < buffs.Count; ++i)
            {
                if (buffs[i].BuffTimeRemaining() == 0)
                {
                    buffs.RemoveAt(i);
                    --i;
                }
            }
        }
        //protected virtual void UpdateOverlays()
        //{
        //    if (stunnedOverlay != null)
        //        stunnedOverlay.gameObject.SetActive(state == "STUNNED");
        //}

    }

}
