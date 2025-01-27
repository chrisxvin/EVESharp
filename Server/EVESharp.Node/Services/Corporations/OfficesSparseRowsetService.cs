﻿using System.Collections.Generic;
using System.Linq;
using EVESharp.Database.Old;
using EVESharp.EVE.Data.Inventory.Items.Types;
using EVESharp.EVE.Network.Services;
using EVESharp.EVE.Notifications;
using EVESharp.EVE.Sessions;
using EVESharp.Node.Client.Notifications.Database;
using EVESharp.Node.Services.Database;
using EVESharp.Types;
using EVESharp.Types.Collections;

namespace EVESharp.Node.Services.Corporations;

public class OfficesSparseRowsetService : SparseRowsetDatabaseService
{
    private         Dictionary <PyDataType, int> RowsIndex = new Dictionary <PyDataType, int> ();
    public override AccessLevel                  AccessLevel   => AccessLevel.None;
    private         Corporation                  Corporation   { get; }
    private         CorporationDB                DB            { get; }
    private         INotificationSender          Notifications { get; }

    public OfficesSparseRowsetService
    (
        Corporation         corporation, CorporationDB db, SparseRowset rowsetHeader, IBoundServiceManager manager, Session session,
        INotificationSender notificationSender
    ) : base (rowsetHeader, manager, session, true)
    {
        DB            = db;
        Corporation   = corporation;
        Notifications = notificationSender;

        // get all the indexes based on the key
        this.RowsIndex = DB.GetOffices (corporation.ID);
    }

    public override PyDataType Fetch (ServiceCall call, PyInteger startPos, PyInteger fetchSize)
    {
        return DB.GetOffices (Corporation.ID, startPos, fetchSize, RowsetHeader);
    }

    public override PyDataType FetchByKey (ServiceCall call, PyList keyList)
    {
        return DB.GetOffices (keyList.GetEnumerable <PyInteger> (), Corporation.ID, RowsetHeader, this.RowsIndex);
    }

    public override PyDataType SelectByUniqueColumnValues (ServiceCall call, PyString columnName, PyList values)
    {
        return DB.GetOffices (columnName, values.GetEnumerable <PyInteger> (), Corporation.ID, RowsetHeader, this.RowsIndex);
    }

    protected override void SendOnObjectChanged (PyDataType primaryKey, PyDictionary <PyString, PyTuple> changes, PyDictionary notificationParams = null)
    {
        // TODO: UGLY CASTING THAT SHOULD BE POSSIBLE TO DO DIFFERENTLY
        // TODO: NOT TO MENTION THE LINQ USAGE, MAYBE THERE'S A BETTER WAY OF DOING IT
        PyList <PyDataType> characterIDs = new PyList <PyDataType> (Sessions.Select (x => (PyDataType) x.Value.CharacterID).ToList ());

        Notifications.NotifyCharacters (
            characterIDs.GetEnumerable <PyInteger> (),
            new OnObjectPublicAttributesUpdated (primaryKey, this, changes, notificationParams)
        );
    }

    public override void AddRow (PyDataType primaryKey, PyDictionary <PyString, PyTuple> changes)
    {
        this.RowsIndex = DB.GetOffices (Corporation.ID);
        // update the header count
        this.RowsetHeader.Count++;

        this.SendOnObjectChanged (primaryKey, changes);
    }

    public override void UpdateRow (PyDataType primaryKey, PyDictionary <PyString, PyTuple> changes)
    {
        this.SendOnObjectChanged (primaryKey, changes);
    }

    public override void RemoveRow (PyDataType primaryKey)
    {
        this.RowsIndex = DB.GetOffices (Corporation.ID);
        // update the header count
        this.RowsetHeader.Count--;

        PyDictionary <PyString, PyTuple> changes = new PyDictionary <PyString, PyTuple>
        {
            ["officeID"] = new PyTuple (2)
            {
                [0] = primaryKey,
                [1] = null
            },
            ["typeID"] = new PyTuple (2)
            {
                [0] = 0,
                [1] = null
            },
            ["stationID"] = new PyTuple (2)
            {
                [0] = 0,
                [1] = null
            },
            ["officeFolderID"] = new PyTuple (2)
            {
                [0] = primaryKey,
                [1] = null
            }
        };

        this.SendOnObjectChanged (primaryKey, changes);
    }

    public override bool IsClientAllowedToCall (Session session)
    {
        return session.CorporationID == Corporation.ID;
    }
}