﻿using UnityEngine;

namespace stuartmillman.dissertation.goap
{
    [CreateAssetMenu(menuName = "GOAP/Actions/Mine Rock")]
    public class GActionMineRock : GAction
    {
        private Rock _rock;

        public GActionMineRock()
        {
            AddPrecondition("has_pickaxe", true);
            AddEffect("stones", 5);
        }

        public override bool Run(GAgent agent)
        {
            if (!_rock.IsInteracting)
            {
                _rock.Interact();
            }
            else
            {
                if (_rock.IsInteractionComplete)
                {
                    return true;
                }
            }

            return false;
        }

        public void SetRock(Rock rock)
        {
            _rock = rock;
            SetCost(_rock.TimeToMine);
            AddPrecondition("at_object", _rock);
        }
    }
}