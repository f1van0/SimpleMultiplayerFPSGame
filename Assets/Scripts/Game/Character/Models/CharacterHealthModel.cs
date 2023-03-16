using Normal.Realtime;
using Normal.Realtime.Serialization;

namespace JoyWay.Game.Character.Components
{
    [RealtimeModel]
    public partial class CharacterHealthModel
    {
        [RealtimeProperty(1, true, false)]
        private int _maxHealth;
        [RealtimeProperty(2, true, true)]
        private int _health;
    }
}

/* ----- Begin Normal Autogenerated Code ----- */
namespace JoyWay.Game.Character.Components {
    public partial class CharacterHealthModel : RealtimeModel {
        public int maxHealth {
            get {
                return _maxHealthProperty.value;
            }
            set {
                if (_maxHealthProperty.value == value) return;
                _maxHealthProperty.value = value;
                InvalidateReliableLength();
            }
        }
        
        public int health {
            get {
                return _healthProperty.value;
            }
            set {
                if (_healthProperty.value == value) return;
                _healthProperty.value = value;
                InvalidateReliableLength();
                FireHealthDidChange(value);
            }
        }
        
        public delegate void PropertyChangedHandler<in T>(CharacterHealthModel model, T value);
        public event PropertyChangedHandler<int> healthDidChange;
        
        public enum PropertyID : uint {
            MaxHealth = 1,
            Health = 2,
        }
        
        #region Properties
        
        private ReliableProperty<int> _maxHealthProperty;
        
        private ReliableProperty<int> _healthProperty;
        
        #endregion
        
        public CharacterHealthModel() : base(null) {
            _maxHealthProperty = new ReliableProperty<int>(1, _maxHealth);
            _healthProperty = new ReliableProperty<int>(2, _health);
        }
        
        protected override void OnParentReplaced(RealtimeModel previousParent, RealtimeModel currentParent) {
            _maxHealthProperty.UnsubscribeCallback();
            _healthProperty.UnsubscribeCallback();
        }
        
        private void FireHealthDidChange(int value) {
            try {
                healthDidChange?.Invoke(this, value);
            } catch (System.Exception exception) {
                UnityEngine.Debug.LogException(exception);
            }
        }
        
        protected override int WriteLength(StreamContext context) {
            var length = 0;
            length += _maxHealthProperty.WriteLength(context);
            length += _healthProperty.WriteLength(context);
            return length;
        }
        
        protected override void Write(WriteStream stream, StreamContext context) {
            var writes = false;
            writes |= _maxHealthProperty.Write(stream, context);
            writes |= _healthProperty.Write(stream, context);
            if (writes) InvalidateContextLength(context);
        }
        
        protected override void Read(ReadStream stream, StreamContext context) {
            var anyPropertiesChanged = false;
            while (stream.ReadNextPropertyID(out uint propertyID)) {
                var changed = false;
                switch (propertyID) {
                    case (uint) PropertyID.MaxHealth: {
                        changed = _maxHealthProperty.Read(stream, context);
                        break;
                    }
                    case (uint) PropertyID.Health: {
                        changed = _healthProperty.Read(stream, context);
                        if (changed) FireHealthDidChange(health);
                        break;
                    }
                    default: {
                        stream.SkipProperty();
                        break;
                    }
                }
                anyPropertiesChanged |= changed;
            }
            if (anyPropertiesChanged) {
                UpdateBackingFields();
            }
        }
        
        private void UpdateBackingFields() {
            _maxHealth = maxHealth;
            _health = health;
        }
        
    }
}
/* ----- End Normal Autogenerated Code ----- */
