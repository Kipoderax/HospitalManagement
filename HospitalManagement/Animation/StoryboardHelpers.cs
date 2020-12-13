﻿using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace HospitalManagement
{
    /// <summary>
    /// Animation helpers for <see cref="Storyboard"/>
    /// </summary>
    public static class StoryboardHelpers
    {
        /// <summary>
        /// Adds a slide from right animation to the storyboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <param name="offset">The distance to the right to start from</param>
        /// <param name="decelerationRation">The rate of deceleration</param>
        public static void AddSlideFromRight( this Storyboard storyboard, float seconds, double offset, float decelerationRation = 0.9f )
        {
            // Create the margin animate from right
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(offset, 0, -offset, 0),
                To = new Thickness(0),
                DecelerationRatio = decelerationRation
            };

            // Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // Add this to the storyboard
            storyboard.Children.Add(animation);
        }

        /// <summary>
        /// Adds a slide from left animation to the storyboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <param name="offset">The distance to the left to start from</param>
        /// <param name="decelerationRation">The rate of deceleration</param>
        public static void AddSlideFromLeft ( this Storyboard storyboard, float seconds, double offset, float decelerationRation = 0.9f )
        {
            // Create the margin animate from right
            var animation = new ThicknessAnimation
            {
                Duration = new Duration( TimeSpan.FromSeconds( seconds ) ),
                From = new Thickness( 0 ),
                To = new Thickness( -offset, 0, offset, 0 ),
                DecelerationRatio = decelerationRation
            };

            // Set the target property name
            Storyboard.SetTargetProperty( animation, new PropertyPath( "Margin" ) );

            // Add this to the storyboard
            storyboard.Children.Add( animation );
        }

        /// <summary>
        /// Adds a fade in animtion to the storyboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time the animation will take</param>
        public static void AddFadeIn(this Storyboard storyboard, float seconds)
        {
            // Create the margin animate from right
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = 0,
                To = 1
            };

            // Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));

            // Add this to the storyboard
            storyboard.Children.Add(animation);
        }

        /// <summary>
        /// Adds a fade out animtion to the storyboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time the animation will take</param>
        public static void AddFadeOut ( this Storyboard storyboard, float seconds )
        {
            // Create the margin animate from right
            var animation = new DoubleAnimation
            {
                Duration = new Duration( TimeSpan.FromSeconds( seconds ) ),
                From = 1,
                To = 0
            };

            // Set the target property name
            Storyboard.SetTargetProperty( animation, new PropertyPath( "Opacity" ) );

            // Add this to the storyboard
            storyboard.Children.Add( animation );
        }
    }
}
