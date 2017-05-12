//Apache2, 2017, WinterDev
//
// Copyright 2010 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// Author: jdtang@google.com (Jonathan Tang)
//
// Coding conventions specific to this file:
//
// 1. Functions that fill in a token should be named emit_*, and should be
// followed immediately by a return from the tokenizer (true if no error
// occurred, false if an error occurred).  Sometimes the emit functions
// themselves return a boolean so that they can be combined with the return
// statement; in this case, they should match this convention.
// 2. Functions that shuffle data from temporaries to final API structures
// should be named finish_*, and be called just before the tokenizer exits the
// state that accumulates the temporary.
// 3. All internal data structures should be kept in an initialized state from
// tokenizer creation onwards, ready to accept input.  When a buffer's flushed
// and reset, it should be deallocated and immediately reinitialized.
// 4. Make sure there are appropriate break statements following each state.
// 5. Assertions on the state of the temporary and tag buffers are usually a
// good idea, and should go at the entry point of each state when added.
// 6. Statement order within states goes:
//    1. Add parse errors, if appropriate.
//    2. Call finish_* functions to build up tag state.
//    2. Switch to new state.  Set _reconsume flag if appropriate.
//    3. Perform any other temporary buffer manipulation.
//    4. Emit tokens
//    5. Return/break.
// This order ensures that we can verify that every emit is followed by a
// return, ensures that the correct state is recorded with any parse errors, and
// prevents parse error position from being messed up by possible mark/resets in
// temporary buffer manipulation.



namespace Gumbo
{


    partial class GumboParser
    {
        bool gumbo_lex(GumboTokenStream output)
        {
            //line:2812
            throw new TODOImplementException();
            //lex current data to output token stream
            //bool gumbo_lex(GumboParser* parser, GumboToken* output)
            //{

            //    // Because of the spec requirements that...
            //    //
            //    // 1. Tokens be handled immediately by the parser upon emission.
            //    // 2. Some states (eg. CDATA, or various error conditions) require the
            //    // emission of multiple tokens in the same states.
            //    // 3. The tokenizer often has to reconsume the same character in a different
            //    // state.
            //    //
            //    // ...all state must be held in the GumboTokenizer struct instead of in local
            //    // variables in this function.  That allows us to return from this method with
            //    // a token, and then immediately jump back to the same state with the same
            //    // input if we need to return a different token.  The various emit_* functions
            //    // are responsible for changing state (eg. flushing the chardata buffer,
            //    // reading the next input character) to avoid an infinite loop.
            //    GumboTokenizerState* tokenizer = parser->_tokenizer_state;

            //    if (tokenizer->_buffered_emit_char != kGumboNoChar)
            //    {
            //        tokenizer->_reconsume_current_input = true;
            //        emit_char(parser, tokenizer->_buffered_emit_char, output);
            //        // And now that we've avoided advancing the input, make sure we set
            //        // _reconsume_current_input back to false to make sure the *next* character
            //        // isn't consumed twice.
            //        tokenizer->_reconsume_current_input = false;
            //        tokenizer->_buffered_emit_char = kGumboNoChar;
            //        return true;
            //    }

            //    if (maybe_emit_from_temporary_buffer(parser, output))
            //    {
            //        return true;
            //    }

            //    while (1)
            //    {
            //        assert(!tokenizer->_temporary_buffer_emit);
            //        assert(tokenizer->_buffered_emit_char == kGumboNoChar);
            //        int c = utf8iterator_current(&tokenizer->_input);
            //        gumbo_debug(
            //            "Lexing character '%c' (%d) in state %d.\n", c, c, tokenizer->_state);
            //        StateResult result =
            //            dispatch_table[tokenizer->_state](parser, tokenizer, c, output);
            //        // We need to clear reconsume_current_input before returning to prevent
            //        // certain infinite loop states.
            //        bool should_advance = !tokenizer->_reconsume_current_input;
            //        tokenizer->_reconsume_current_input = false;

            //        if (result == RETURN_SUCCESS)
            //        {
            //            return true;
            //        }
            //        else if (result == RETURN_ERROR)
            //        {
            //            return false;
            //        }

            //        if (should_advance)
            //        {
            //            utf8iterator_next(&tokenizer->_input);
            //        }
            //    }
            //}
        }

    }
}