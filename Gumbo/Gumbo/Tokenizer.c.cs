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


