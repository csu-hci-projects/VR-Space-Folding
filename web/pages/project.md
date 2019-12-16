# VR Space Folding

CS 567 (Fall 2019) Class Project  
Joe Strout

# Description

_VR space folding_ is a new approach to making a virtual environment navigable by real walking.  It is a type of _impossible space_, that is, a layout that would not be possible in the real world.  In brief, the layout consists of a series of rooms connected by portals, enabling multiple rooms to occupy the same physical space.  To explore this concept, I implemented a prototype layout consisting of five virtual rooms folded into a 3 x 4 meter physical space, using the Unity development environment.  Subjects were tasked with collecting stars which appeared in random order throughout the virtual space.  Exit surveys assessed the subject's comfort and ability to navigate the impossible space.

# Motivation

Real walking is in many ways an ideal way to navigate an environment: it feels natural, is both quick and precise, and may reduce motion sickness because vestibular and visual cues remain naturally synchronized.  However, because users have only a limited physical area to walk around in, real walking may severely limit the design of a virtual environment.

Many approaches have been proposed to overcome this problem, but each has its own limitations.  Redirected walking still requires a very large physical area (at least 35 x 35 m).  Change blindness redirection relies on the user not coming to know the layout well — an unlikely prospect in most games — and requires the user to follow certain paths, rather than explore freely.  Earlier work on impossible spaces relied on expanding portions of the environment when not in view, but got at most a 56% gain in usable area.

VR space folding, in comparison, has a number of benefits:

- unlimited spatial gain
- works in a physical area as small as 3 x 2 m
- allows free exploration
- allows users to come to know the space well

This project takes the idea from concept to prototype, to see how users actually respond.

# Methodology

The VR space folding technique relies on _portals_, rectangular areas of space that allow the user to see and travel to some other dimension or part of the virtual space.  So the project began by searching for off-the-shelf portal solutions for Unity.  Unfortunately, none of the portal solutions tried worked properly in VR (at least not on the target platform of Oculus Quest).  A custom portal solution was therefore written which relies on a special shader.  This shader can limit its rendering to only parts of space that would be visible on one side or another of a portal, defined by a line segment in the ground plane and a forward direction.

![WorldClipShader](images/WorldClipShader.png "images/WorldClipShader.png")

The image above illustrates the operation of this shader.  The current scene consists of two materials, shaded red and yellow here for clarity, representing two rooms of the virtual environment.  The shader for each material has been configured to represent the portal in front of the camera.  The yellow material is configured to draw only content on the far side of the portal; the red material is configured to do the opposite.

Using these portals, a 5-room virtual layout was designed, representing a simple sci-fi style spaceship.

![Layout3D](images/Layout3D.png "images/Layout3D.png")

The rooms are separated in the design environment, but when the scene launches, a script moves them all into the same space, relying on the other scripts to hide all but the current room and any room visible through a portal, and configure the shader for any portal in view.

Users are given a navigation task as follows.  A round cabinet was placed in each room, labeled with the name of the room, and sometimes containing a pick-up target (a bouncing star).  During the task, one of the room names appears over the user's left-hand controller, indicating the room in which a star can be found.  The user navigates to that room and touches the star with either controller, whereupon it disappears, and the next room is identified in the same manner.

![Pickup](images/Pickup.png "images/Pickup.png")

Each subject was introduced to the environment with a brief explanation of how to don and adjust the headset, and how to perform the task.  Subjects were given as much time as needed to locate a star in each room of the ship, and then were instructed to continue for a 5-minute trial.  Headset position and orientation, as well as pickup events, were logged to a file during the trial.  After five minutes, subjects were helped out of the VR gear, and then given the NASA TLX survey as well as a custom survey.

All experiments were done on a Quest headset, programmed in Unity on a MacBook Pro.  (All models in the experiment, except for the Oculus hand controllers, were modeled by the author.)

# Results

Results of the Likert-scale survey are summarized in the chart below.  Positive statements are grouped at the top, and negative statements at the bottom, though in the actual survey these were interleaved.  

![LikertChart](images/LikertChart.png "images/LikertChart.png")

There was strong agreement with the positive statements, such as “I was able to navigate the space without much trouble.”  There was mostly disagreement or neutrality on the negative statements.

The subjects were also given an opportunity to provide free-form comments.  A representative sample of these:

- “it felt oddly natural”

- “this shouldn’t feel normal, but it does”	

- “I understood where each of the rooms were”

- “would have liked another 5 minutes or so”

Together with the Likert results, these suggest that users were on the whole not bothered by the impossible nature of the folded space.

# Conclusion

This project prototyped a new form of spatial compression with significant advantages over previous approaches.  It works with a physical space as small as 2 by 3 meters, compatible with most home room-scale setups.  Yet it allows for an unlimited amount of virtual space, which users can navigate by real walking.  Finally, the pilot study suggests that users actually like it.  For these reasons, space folding may be expected to become an important technique in VR software design.

# Links

- [Report](https://github.com/csu-hci-projects/VR-Space-Folding/blob/master/paper/Strout-CS567.pdf)
- [GitHub Repo](https://github.com/csu-hci-projects/VR-Space-Folding)
- [Brief Project Video](https://youtu.be/YtcG0lH2q-E)
- [Full Video Presentation](https://youtu.be/XZIql_T9iys)


