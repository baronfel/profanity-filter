﻿// Copyright (char) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace ProfanityFilter.Services;

/// <summary>
/// Provides match evaluators for replacing profanity matches with asterisks or emojis.
/// </summary>
internal static class MatchEvaluators
{
    /// <summary>
    /// A <see cref="MatchEvaluator"/> that replaces a matched string with asterisks.
    /// </summary>
    internal static MatchEvaluator AsteriskEvaluator = new(
        static match =>
        {
            var result = new string('*', match.Length);

            return result;
        });

    
    /// <summary>
    /// A <see cref="MatchEvaluator"/> that replaces a matched profanity with a random emoji from 
    /// a predefined list of hand-selected replacements.
    /// </summary>
    internal static MatchEvaluator EmojiEvaluator = new(
        static match =>
        {
            var emoji = Emoji.HandSelectedReplacements;

            return emoji[Random.Shared.Next(emoji.Length)];
        });

    /// <summary>
    /// A <see cref="MatchEvaluator"/> that replaces a matched string with a random number of asterisks.
    /// The number of asterisks is between 1 and the length of the matched string.
    /// </summary>
    internal static MatchEvaluator RandomAsteriskEvaluator = new(
        static match =>
        {
            var result = new string('*', Random.Shared.Next(1, match.Length));

            return result;
        });
    
    
    /// <summary>
    /// A <see cref="MatchEvaluator"/> that replaces the characters between the first and last 
    /// characters of a match with asterisks.
    /// </summary>
    internal static MatchEvaluator MiddleAsteriskEvaluator = new(
        static match =>
        {
            var value = match.ValueSpan;

            var result = $"{value[0]}{new string('*', match.Length - 2)}{value[^1]}";

            return result;
        });

    /// <summary>
    /// A <see cref="MatchEvaluator"/> that replaces the middle of a swear word with the 🤬 emoji.
    /// </summary>
    internal static MatchEvaluator MiddleSwearEmojiEvaluator = new(
        static match =>
        {
            var value = match.ValueSpan;

            var result = $"{value[0]}🤬{value[^1]}";

            return result;
        });

    /// <summary>
    /// A <see cref="MatchEvaluator"/> that replaces vowels in a string with asterisks (*).
    /// </summary>
    internal static MatchEvaluator VowelAsteriskEvaluator = new(
        static match =>
        {
            var value = match.ValueSpan;

            var result = new StringBuilder(match.Length);

            for (var index = 0; index < match.Length; ++ index)
            {
                var @char = value[index];
                result.Append(@char.IsVowel() ? '*' : @char);
            }

            return result.ToString();
        });
}
