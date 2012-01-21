using Library;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tests
{
	[TestFixture]
	public class ControllerTests
	{
		private CustomerController sut;

		private IView view;
		private ICustomerService customerService;

		[SetUp]
		public void SetUp()
		{
			view = MockRepository.GenerateStub<IView>();
			customerService = MockRepository.GenerateStub<ICustomerService>();

			sut = new CustomerController(customerService);
			sut.View = view;
		}

		[Test]
		public void ViewIsUpdatedWithCustomerDetailsWhenRefreshIsCalled()
		{
			// Arrange
			var customerId = 1;
			customerService.Expect(cs => cs.GetCustomer(customerId)).Return(new Customer { Id = customerId });

			// Act
			sut.Refresh(customerId);

			// Assert
			var args = view.GetArgumentsForCallsMadeOn(v => v.BindCustomerData(null), mo => mo.IgnoreArguments());
			var customerDTO = args[0][0] as CustomerDTO;
			Assert.That(customerDTO.Id, Is.EqualTo(customerId));
		}
	}
}
