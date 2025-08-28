require 'rspec'
require_relative '../lib/my_methods'

RSpec.describe 'My Methods' do
  describe '#dig_pow' do
    it 'checks the digits power method for correctness' do
      expect(dig_pow(89, 1)).to eq(1)
    end
  end
end