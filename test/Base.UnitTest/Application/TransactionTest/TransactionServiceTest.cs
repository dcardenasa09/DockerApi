using Xunit;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using Base.API.Application.Transactions.Services;
using Base.API.Application.Transactions.Domain.DTO;
using Base.Domain.AggregatesModel.TransactionAggregate;
using Base.Domain.AggregatesModel.DomainEvents;

namespace Base.API.Tests.Application.Transactions.Services
{
    public class TransactionServiceTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
        private readonly TransactionService _transactionService;

        public TransactionServiceTests()
        {
            _mapperMock = new Mock<IMapper>();
            _mediatorMock = new Mock<IMediator>();
            _transactionRepositoryMock = new Mock<ITransactionRepository>();
            _transactionService = new TransactionService(_mapperMock.Object, _mediatorMock.Object, _transactionRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateTransactionWithEventPublication() {
            var existingTransaction = new Transaction {
                Id              = 1,
                SavingAccountId = 12345,
                Date            = DateTime.Now.AddDays(-1),
                Amount          = 100,
                Type            = 1,
                Status          = 1,
                Observations    = "Test observation",
                IsActive        = true,
                Folio           = "TRS-1"
            };

            var transactionList = new List<Transaction> { existingTransaction };
            _transactionRepositoryMock.Setup(r => r.GetList(It.IsAny<Expression<Func<Transaction, bool>>>(), null, true))
                                      .Returns<Expression<Func<Transaction, bool>>, string[], bool>((predicate, includes, applyAsNoTracking) => {
                                            var filteredList = transactionList.AsQueryable().Where(predicate).ToList();
                                            return Task.FromResult(filteredList);
                                        });

            _transactionRepositoryMock.Setup(r => r.Create(It.IsAny<Transaction>()))
                                      .ReturnsAsync((Transaction entity) => {
                                            entity.Folio = GenerateNextFolio(transactionList);
                                            transactionList.Add(entity);
                                            return entity;
                                        });

            _mapperMock.Setup(m => m.Map<Transaction>(It.IsAny<TransactionDTO>())).Returns<TransactionDTO>(dto => {
                var transaction = MapDtoToTransaction(dto);
                return transaction;
            });

            _mapperMock.Setup(m => m.Map<TransactionDTO>(It.IsAny<Transaction>())).Returns<Transaction>(transaction => {
                var dto = MapEntityToTransactionDTO(transaction);
                return dto;
            });

            var result = await _transactionService.Create(new TransactionDTO {
                Id              = 0,
                SavingAccountId = 12345,
                Date            = DateTime.Now,
                Amount          = 200,
                Type            = 2,
                Status          = 1,
                Observations    = "New transaction",
                IsActive        = true,
                Folio           = null,
                SavingAccount   = null
            });


            Assert.NotNull(result);
            Assert.Equal(200, result.Amount);
            Assert.Equal(2, result.Type);
            Assert.Equal(1, result.Status);
            Assert.Equal("New transaction", result.Observations);
            Assert.True(result.IsActive);
            Assert.NotNull(result.Folio);

            _transactionRepositoryMock.Verify(r => r.Create(It.IsAny<Transaction>()), Times.Once);
            _mediatorMock.Verify(m => m.Publish(It.IsAny<INotification>(), It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        private static TransactionDTO MapEntityToTransactionDTO(Transaction transaction) {
            return new TransactionDTO {
                Id              = transaction.Id,
                SavingAccountId = transaction.SavingAccountId,
                Date            = transaction.Date,
                Amount          = transaction.Amount,
                Type            = transaction.Type,
                Status          = transaction.Status,
                Observations    = transaction.Observations,
                IsActive        = transaction.IsActive,
                Folio           = transaction.Folio
            };
        }

        private static Transaction MapDtoToTransaction(TransactionDTO dto) {
            return new Transaction {
                Id              = dto.Id,
                SavingAccountId = dto.SavingAccountId,
                Date            = dto.Date,
                Amount          = dto.Amount,
                Type            = dto.Type,
                Status          = dto.Status,
                Observations    = dto.Observations,
                IsActive        = dto.IsActive,
                Folio           = dto.Folio
            };
        }

        private static string GenerateNextFolio(List<Transaction> transactions) {
            int maxNumero = transactions.Max(t => GetNumeroFromFolio(t.Folio));
            return $"TRS-{maxNumero + 1}";
        }

        private static int GetNumeroFromFolio(string folio) {
            var parts = folio?.Split('-');
            if (parts?.Length == 2 && int.TryParse(parts[1], out int numero)) {
                return numero;
            }

            return 0;
        }
    }
}
