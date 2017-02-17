using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Objects.PlayerRelated
{
    class WeaponStates
    {
        public enum WeaponType
        {
            None,
            Pistol,
            ShotGun,
            MachineGun,
            Missile,
            Lazer,
        }
        public WeaponType type;

        public WeaponStates()
        {
            this.type = WeaponType.None;
        }

        public WeaponStates(WeaponType weaponState)
        {
            this.type = weaponState;
        }

        public void Update(float time, ref float shotInterval)
        {
            HandleWeaponStates(time, ref shotInterval);
        }

        private void HandleWeaponStates(float time, ref float shotInterval)
        {
            switch (type)
            {
                case WeaponType.Pistol:
                    shotInterval += time;
                    break;
                case WeaponType.ShotGun:
                    shotInterval += time * 0.5f;
                    break;
                case WeaponType.MachineGun:
                    shotInterval += time * 2.5f;
                    break;
            }
        }

    }
}
