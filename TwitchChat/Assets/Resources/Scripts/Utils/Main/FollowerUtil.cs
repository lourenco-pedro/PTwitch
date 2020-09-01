using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwitchChat_frntEnd
{
    public static class FollowerUtil
    {
        public static void UpdateFollowers()
        {
            TryEnqueueNewFollower();
            TryDequeueNewFollowers();
            UpdateNewFollower();
        }

        public static Follower CreateFollower(string username, FollowerMessageInfo message)
        {
            Follower followerToEnqueue = new Follower();
            followerToEnqueue.Messages = new List<FollowerMessageInfo>();

            followerToEnqueue.UserName = username;
            followerToEnqueue.Messages.Add(message);

            return followerToEnqueue;
        }

        private static void TryEnqueueNewFollower()
        {
            Simulation simulation = SimulationUtil.GetSimulation();

            Client loadedClient = ClientUtil.GetClient();
            ClientUtil.LoadClient(out loadedClient);

            Client localClient = ClientUtil.GetClient();

            for (int i = 0; i < loadedClient.Followers.Count; i++)
            {
                Follower loadedFollower = loadedClient.Followers[i];
                Follower clientFollower = null;

                if (ExistInClient(out clientFollower, loadedFollower))
                {
                    if (loadedFollower.Messages.Count != clientFollower.Messages.Count)
                    {
                        clientFollower.Messages.Add(loadedFollower.Messages[loadedFollower.Messages.Count - 1]);
                    }

                    continue;
                }

                Follower followerToEnqueue = CreateFollower(loadedFollower.UserName, loadedFollower.Messages[loadedFollower.Messages.Count - 1]);

                localClient.Followers.Add(followerToEnqueue);
                Debug.Log("Added client to chat: " + followerToEnqueue.UserName);

                simulation.FollowersToAdd.Enqueue(followerToEnqueue);
            }
        }

        private static bool ExistInClient(out Follower follower, Follower target)
        {
            follower = null;

            Client client = ClientUtil.GetClient();

            for (int i = 0; i < client.Followers.Count; i++)
            {
                var curFollower = client.Followers[i];
                if (curFollower.UserName == target.UserName)
                {
                    follower = curFollower;
                    return true;
                }
            }

            return false;
        }

        private static void TryDequeueNewFollowers()
        {
            Simulation simulation = SimulationUtil.GetSimulation();
            Client client = ClientUtil.GetClient();

            if (!string.IsNullOrEmpty(simulation.NewFollower.UserName) || simulation.FollowersToAdd.Count == 0)
                return;

            simulation.NewFollower = simulation.FollowersToAdd.Dequeue();
        }

        private static void UpdateNewFollower()
        {
            Simulation simulation = SimulationUtil.GetSimulation();
            Client client = ClientUtil.GetClient();

            if (string.IsNullOrEmpty(simulation.NewFollower.UserName))
                return;

            simulation.CurrentTimeDisplayFollower += Time.deltaTime / simulation.TimeDisplayFollower;

            if (simulation.CurrentTimeDisplayFollower >= 1)
            {

                simulation.NewFollower = new Follower();
                simulation.NewFollower.Messages = new List<FollowerMessageInfo>();

                simulation.CurrentTimeDisplayFollower = 0;
            }
        }
    }
}
