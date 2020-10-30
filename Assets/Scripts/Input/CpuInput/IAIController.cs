/// <summary>
/// This class is responsible for passing Input from the player to the Active character class. IUnitController is the only public portion of 
/// this class so you should only call this via the interface
/// Other classes to control AI input need to implement the IUnitcontroller separately
/// </summary>

namespace Assets.Scripts.Units
{
    public interface IAIController
    {
        void InititateLightAttack();
        void InititateHeavyAttack();

        void InititateSkillOne();
        void InititateSkillTwo();
        void InititateSkillThree();
        void InititateSkillFour();

        void InititateManeuver();
    }
}
