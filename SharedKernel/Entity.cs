using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackableEntities.Common.Core;

namespace SharedKernel
{
    public abstract class Entity : ITrackable, IMergeable
    {
        protected Entity()
        {
        }

        int? _requestedHashCode;
        int _Id;

        public virtual int Id
        {
            get
            {
                return _Id;
            }
            protected set
            {
                _Id = value;
            }
        }

        public void ResetAsNew()
        {
            Id = 0;
            TrackingState = TrackingState.Added;
        }
        private List<INotification> _domainEvents;
        public List<INotification> DomainEvents => _domainEvents;

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }
        public void RemoveDomainEvent(INotification eventItem)
        {
            if (_domainEvents is null) return;
            _domainEvents.Remove(eventItem);
        }

        /// <summary>
        /// Returns TRUE if the item has not yet been persisted
        /// </summary>
        /// <returns></returns>
        public bool IsTransient()
        {
            return this.Id == default(Int32);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            Entity item = (Entity)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Id == this.Id;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }


        // Trackable Entities Related ///////////////////////////////////////////////////////////////////////////////////
        public TrackingState TrackingState { get; set; }

        public ICollection<string> ModifiedProperties { get; set; }

        public Guid EntityIdentifier { get; set; }

        /////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
