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
            Pistol,
            ShotGun,
            MachineGun,
            Missile,
            Lazer,
        }
        public WeaponType type;

        int weaponLevel;
        
        public WeaponStates()
        {
            type = WeaponType.Pistol;
            weaponLevel = 0;
        }

        public void Update(float time, ref float shotInterval)
        {
            HandleWeaponStates(time, shotInterval);
        }

        public void UpgradeWeapon()
        {
            ++weaponLevel;
            if (weaponLevel == 1)
                type = WeaponType.Pistol;
            else if (weaponLevel == 2)
                type = WeaponType.MachineGun;
            else if (weaponLevel == 3)
                type = WeaponType.ShotGun;
        }

        private void HandleWeaponStates(float time, float shotInterval)
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
