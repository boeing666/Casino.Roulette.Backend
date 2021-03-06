﻿using System.Threading.Tasks;
using Casino.Roulette.Backend.Contracts.Messages;
using Casino.Roulette.Backend.Contracts.Models.Messages;
using Casino.Roulette.Backend.Models;
using Casino.Roulette.Backend.Services;
using Microsoft.AspNetCore.SignalR;

namespace Casino.Roulette.Backend.Hubs
{
    public class RouletteHub : Hub
    {
        private readonly IRouletteEngine _engine;

        public RouletteHub(IRouletteEngine engine)
        {
            _engine = engine;
        }


        public override async Task OnConnectedAsync()
        {
            var requestQuery = Context.GetHttpContext().Request.Query;
            if (requestQuery.ContainsKey("token"))
            {
                var token = requestQuery["token"];
                _engine.ConnectToRoulette(token, Context.ConnectionId);
            }

            if (requestQuery.ContainsKey("access_token"))
            {
                var token = requestQuery["access_token"];
                _engine.ConnectToRoulette(token, Context.ConnectionId);
            }

        }

        public BaseResponse InitApp()
        {
            var result = new BaseResponse();
            if (!_engine.TryGetUserByConnectionId(Context.ConnectionId, out var user))
            {
                result.Message = "Invalid Connection";
                result.Success = false;
            }
            else
            {
                result.Data = user.GetUserModel();
            }

            return result;
        }

        public BaseResponse JoinTable(long tableId)
        {
            var result = new BaseResponse();
            if (!_engine.TryGetUserByConnectionId(Context.ConnectionId, out var user))
            {
                result.Message = "user not connected";
                result.Success = false;
                return result;
            }

            if (!_engine.TryConnectToRouletteTable(tableId, user))
            {
                result.Message = "Wrong tableId";
                result.Success = false;
                return result;
            }

            result.Data = _engine.GetTableData(tableId, user.Id);

            return result;
        }

        public BaseResponse BetRequest(BetRequestModel betModel)
        {
            var result = new BaseResponse();
            if (!_engine.TakePlayerBet(betModel))
            {
                result.Success = false;
                result.Message = "Wrong Table State";
            }

            result.Success = true;
            result.Message = "Your bet has been accepted";

            return result;
        }


    }
}
