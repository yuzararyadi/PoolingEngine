using PoolingEngine.DataAccess.Context;
using PoolingEngine.Domain.Entities;
using PoolingEngine.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Opc.UaFx.Client;
using Opc.UaFx;
using System.Net.Sockets;
using PoolingEngine.Domain.Entities.Enum;

namespace PoolingEngine.DataAccess.Implementation
{
    public class OpcUaRepository : IOpcUaRepository
    {
        private OpcClient _opcClient;
        
        public OpcUaRepository()
        {
            _opcClient = new OpcClient("opc.tcp://SG9780105889137:49320");
            _opcClient.Security.UserIdentity = new OpcClientIdentity("yuzar", "yuzararyadiadmin");
        }
        private void OpcUAConnect()
        {
            _opcClient.Connect();
            if(!(_opcClient.State == OpcClientState.Connected || _opcClient.State == OpcClientState.Reconnected))
            {
                Task.Delay(5000).Wait();
                OpcUAConnect();
            }
                
        }
        public IEnumerable<OpcValue>? OpcRead(DeviceItem deviceItem, List<TagDef> tagDefs)
        {
            try
            {
                if (!(_opcClient.State == OpcClientState.Connected || _opcClient.State == OpcClientState.Reconnected))
                    OpcUAConnect();
                List<OpcNodeId> nodeIds = new List<OpcNodeId>();
                foreach (TagDef tagDef in tagDefs)
                {
                    nodeIds.Add(tagDef.MapAddress);
                }
                if (nodeIds.Count > 0)
                {
                    var value = _opcClient.ReadNodes(nodeIds);
                    return value;
                }
                else return null;
                
            }
            catch 
            {
                return null;
            }
            
        }
        public void OpcCommand()
        {
            throw new NotImplementedException();
        }

        public void OpcWrite(DeviceItem deviceItem, TagDef tagDef, WriteItem writeItem, EnumRequestType enumRequestType)
        {
            try
            {
                if (!(_opcClient.State == OpcClientState.Connected || _opcClient.State == OpcClientState.Reconnected))
                    OpcUAConnect();
                OpcNodeId nodeId = tagDef.MapAddress;
                if (enumRequestType == EnumRequestType.WRITE)
                {
                    int value = (int)writeItem.Value;
                    _opcClient.WriteNode(nodeId, value);
                }
                else
                {
                    bool value = (bool)writeItem.Value;
                    _opcClient.WriteNode(nodeId, value);
                }

            }
            catch
            {
                return;
            }
        }
    }
}
