/*
Copyright (c) 2015 Eric Begue (ericbeg@gmail.com)

This source file is part of the Panda BT package, which is licensed under
the Unity's standard Unity Asset Store End User License Agreement ("Unity-EULA").

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/


namespace Panda
{
    public class PandaTree
    {
        internal BTTreeProxy _tree;


        /// <summary>
        /// The tree name
        /// </summary>
        public string name
        {
            get
            {
                return _tree.name;
            }
        }

        /// <summary>
        /// Tree status
        /// </summary>
        public Panda.Status status
        {
            get
            {
                return _tree.status;
            }
        }

        /// <summary>
        /// Tick the tree
        /// </summary>
        public void Tick()
        {
            _tree.Tick();
        }

        /// <summary>
        /// Reset the tree
        /// </summary>
        public void Reset()
        {
            _tree.Reset();
        }


    
    }
}

