﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VocabularyBooster.Service {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class WordCypherQueries {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal WordCypherQueries() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("VocabularyBooster.Service.WordCypherQueries", typeof(WordCypherQueries).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///			UNWIND $word as WordParam 
        ///			MERGE (newWord: Word {expression: WordParam.expression})
        ///			ON CREATE SET
        ///				newWord.uuid = randomUUID(),
        ///				newWord.created = timestamp()
        ///			ON MATCH SET
        ///				newWord.modified = timestamp()
        ///			WITH newWord, WordParam
        ///			UNWIND WordParam.sense as sense
        ///			MERGE (newSense: Sense {definition: sense.definition})
        ///			ON CREATE SET
        ///				newSense.definition = sense.definition,
        ///				newSense.grammaticalCategories = sense.grammaticalCategories,
        ///				newSense.example = se [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string AddOrUpdate {
            get {
                return ResourceManager.GetString("AddOrUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///			UNWIND $cards as CardParam 
        ///			MERGE (card: Card {cardId: CardParam.cardId})
        ///			ON CREATE SET
        ///				card = CardParam,
        ///				card.uuid = randomUUID()
        ///			ON MATCH SET
        ///				card = CardParam
        ///
        ///			MERGE (word:Word {expression: CardParam.keyword})
        ///			MERGE (card)-[r:HAS_WORD]-&gt;(word)
        ///
        ///			MERGE (user: User {email: $userEmail})
        ///			MERGE (user)-[hasCard:HAS_CARD]-&gt;(card)
        ///			SET
        ///				hasCard.factor = CardParam.factor,
        ///				hasCard.interval = CardParam.interval,
        ///				hasCard.note = CardParam.note,
        ///				has [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string AddOrUpdateCard {
            get {
                return ResourceManager.GetString("AddOrUpdateCard", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///			UNWIND $text as nText
        ///			call nlp.lemmatize_text(nText.content) YIELD uuid, id, success
        ///			WITH id, nText
        ///			MATCH (newText: Text)
        ///			WHERE id(newText) = id
        ///			SET newText = nText
        ///			RETURN DISTINCT newText.uuid as textUuid
        ///		.
        /// </summary>
        internal static string AddText {
            get {
                return ResourceManager.GetString("AddText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///			UNWIND $user as user
        ///			CREATE (newUser: User)
        ///				SET newUser = user,
        ///					newUser.uuid = randomUUID(),
        ///					newUser.created = timestamp()
        ///			RETURN newUser.uuid as userUuid
        ///		.
        /// </summary>
        internal static string AddUser {
            get {
                return ResourceManager.GetString("AddUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///			MATCH (card: Card {cardId: $cardId})
        ///			RETURN card{.*} as card
        ///		.
        /// </summary>
        internal static string GetCardByCardId {
            get {
                return ResourceManager.GetString("GetCardByCardId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///			MATCH (text: Text {uuid: $textUuid})
        ///			return text as Text
        ///		.
        /// </summary>
        internal static string GetText {
            get {
                return ResourceManager.GetString("GetText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///			MATCH (user: User {uuid: $userUuid})
        ///			RETURN user{.*} as user
        ///		.
        /// </summary>
        internal static string GetUser {
            get {
                return ResourceManager.GetString("GetUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///			MATCH (user: User {email: $email})
        ///			RETURN user{.*} as user
        ///		.
        /// </summary>
        internal static string GetUserByEmail {
            get {
                return ResourceManager.GetString("GetUserByEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///			MATCH (word: Word {expression: $expression})-[:HAS_SENSE]-&gt;(sense: Sense)
        ///			with word, collect(sense) as senses
        ///			return {expression: word.expression, sense: senses} as word
        ///		.
        /// </summary>
        internal static string GetWord {
            get {
                return ResourceManager.GetString("GetWord", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///			MATCH (text: Text {uuid: $textUuid})-[:HAS_WORD]-&gt;(word:Word)
        ///			return word as Word
        ///		.
        /// </summary>
        internal static string GetWordListFromText {
            get {
                return ResourceManager.GetString("GetWordListFromText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///			MATCH (user: User {uuid:$userUuid})
        ///			MATCH (text: Text {uuid:$textUuid})
        ///			WITH user, text
        ///			MATCH (text)-[:HAS_WORD]-&gt;(word: Word)
        ///			MERGE (user)-[:LEARNED]-&gt;(word)
        ///			RETURN DISTINCT user.uuid
        ///		.
        /// </summary>
        internal static string MakeTextLearned {
            get {
                return ResourceManager.GetString("MakeTextLearned", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///			MATCH (text:Text)
        ///			WHERE text.content =~ &quot;.*&quot; + $phrase + &quot;.*&quot;
        ///			RETURN text{.*} as Text
        ///		.
        /// </summary>
        internal static string SearchText {
            get {
                return ResourceManager.GetString("SearchText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///			MATCH (word: Word)-[:HAS_SENSE]-&gt;(sense: Sense)
        ///			WHERE word.expression =~ &quot;.*&quot; + $expression + &quot;.*&quot;
        ///			WITH word, collect(sense{.*}) as senses
        ///			RETURN {expression: word.expression, sense: senses} as word
        ///		.
        /// </summary>
        internal static string SearchWord {
            get {
                return ResourceManager.GetString("SearchWord", resourceCulture);
            }
        }
    }
}
