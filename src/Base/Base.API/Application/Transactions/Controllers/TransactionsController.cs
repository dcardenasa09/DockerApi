using Microsoft.AspNetCore.Mvc;
using Shared.Lib.Controllers;
using Shared.Lib.Helpers.Validator;
using Base.API.Application.Transactions.Services;
using Base.API.Application.Transactions.Domain.DTO;
using Base.Domain.AggregatesModel.TransactionAggregate;

namespace Base.API.Application.Transactions.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController(ITransactionService transactionService, IValidatorHelper<TransactionDTO> validator) : BaseController<Transaction, TransactionDTO, ITransactionService>(transactionService, validator) {
    private readonly ITransactionService _transactionService = transactionService;
}